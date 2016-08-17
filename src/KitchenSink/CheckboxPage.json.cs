using Starcounter;

namespace KitchenSink {
  partial class CheckboxPage : Page
    {
    public string CalculatedDrivingLicenseReaction {
      get {
        if (DrivingLicense == true) {
          return "You can drive";
        }
        else {
          return "You can't drive";
        }
      }
    }
  }
}
