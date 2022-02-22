using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using FreightChargeApp.Data;
using FreightChargeApp.Domain;
using FreightChargeApp.Models;

namespace FreightChargeApp.ViewModels
{
    public class IndexViewModel : ReactiveObject
    {
        private readonly IDbContextFactory<ShippingContext> contextFactory;
        public readonly CourierModel[] Couriers;

        public IndexViewModel(IDbContextFactory<ShippingContext> contextFactory)
        {
            this.contextFactory = contextFactory;

            PostPackage = ReactiveCommand.CreateFromTask<CourierModel>(SaveShippingDataImpl);

            this.WhenAnyValue(x => x.Length, x => x.Width, x => x.Height,
                    (length, width, height) => new PackageDimensions(length, width, height))
                .ToPropertyEx(this, x => x.PackageDimensions);

            IObservable<PackageDimensions> packageDimensionsObservable = this.WhenAnyValue(x => x.PackageDimensions)
                .WhereNotNull()
                .Replay(1)
                .RefCount();

            IObservable<float> weightObservable = this.WhenAnyValue(x => x.Weight)
                .Replay(1)
                .RefCount();

            Couriers = Enum.GetValues<CourierType>()
                .Select(courierType => new CourierModel(courierType, packageDimensionsObservable, weightObservable))
                .ToArray();
        }

        public ReactiveCommand<CourierModel, Unit> PostPackage { get; }

        [Reactive]
        public float Length { get; set; }

        [Reactive]
        public float Width { get; set; }

        [Reactive]
        public float Height { get; set; }

        [Reactive]
        public float Weight { get; set; }

        [ObservableAsProperty]
        public PackageDimensions? PackageDimensions { get; }

        private async Task SaveShippingDataImpl(CourierModel courierModel)
        {
            if (courierModel.Cost is null)
            {
                return;
            }

            await using ShippingContext context = contextFactory.CreateDbContext();

            Data.Courier courier = await context.Couriers
                .SingleAsync(x => x.Name == courierModel.CourierType.ToString());

            ShippingData shippingData = new(courier.Id, DateTimeOffset.Now, Weight, Length, Width, Height,
                courierModel.Cost.Value);
            await context.ShippingData.AddAsync(shippingData);

            await context.SaveChangesAsync();
        }
    }
}
