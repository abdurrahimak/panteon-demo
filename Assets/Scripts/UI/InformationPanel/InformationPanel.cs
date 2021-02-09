using Panteon.Creation;
using Panteon.Data;

namespace Panteon.UI
{
    public class InformationPanel : IInformationPanel
    {
        InformationPanelView _view;

        public InformationPanel()
        {
            _view = UIFactory.Instance.CreateInformationPanelView();
            _view.Show(false);
        }

        public void SetInformations(InformationData informationData)
        {
            _view.SetInformations(informationData);
        }

        public void Show(bool show)
        {
            _view.Show(show);
        }
    }
}
