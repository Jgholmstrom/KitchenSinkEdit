using Starcounter;

namespace KitchenSink {
  partial class TextareaPage : Page
    {

    public string CalculatedBioReaction {
      get {
        return "Length of your bio: " + Bio.Length + " chars";
      }
    }
  }
}
