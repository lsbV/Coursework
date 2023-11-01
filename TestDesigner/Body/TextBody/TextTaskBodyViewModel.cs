using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using TestDesigner.ViewLib;
using TestLib.Classes.Bodies;
using TestLib.Abstractions;

namespace TestDesigner.Body
{
    public partial class TextTaskBodyViewModel : BaseBodyViewModel
    {
        #region Fields
        [ObservableProperty] private string text = string.Empty;

        #endregion Fields

        #region Properties

        #endregion Properties

        #region Constructors
        public TextTaskBodyViewModel()
        {
            Name = "Text body";
        }

        public override TaskBody CreateBody()
        {
            return new TextTaskBody(Text);
        }
        #endregion Constructors

        #region Commands

        #endregion Commands
    }
}
