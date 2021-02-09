using Panteon.Data;

namespace Panteon.UI
{
    public interface IInformationPanel
    {
        void SetInformations(InformationData informationData);
        void Show(bool show);
    }
}
