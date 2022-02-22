using System.Reactive.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using FreightChargeApp.Models;
using FreightChargeApp.ViewModels;

namespace FreightChargeApp.Views
{
    public partial class Index
    {
        [Inject]
        public IndexViewModel? IndexViewModel
        {
            get => ViewModel;
            set => ViewModel = value;
        }

        public bool CalculatedCost { get; set; }

        public void CalculateCost()
            => CalculatedCost = true;

        public async Task PostPackageAsync(CourierModel courierModel)
            => await ViewModel!.PostPackage.Execute(courierModel);
    }
}
