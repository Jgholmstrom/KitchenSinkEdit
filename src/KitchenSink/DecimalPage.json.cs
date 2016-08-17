using Starcounter;

namespace KitchenSink {
    partial class DecimalPage : Page
    {
        protected override void OnData() {
            base.OnData();

            this.Price = 10;
        }

        public string CalculatedPriceReaction {
            get {
                return "5% of tax is " + (Price / 20);
            }
        }
    }
}
