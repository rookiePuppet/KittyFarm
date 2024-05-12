using KittyFarm.Data;

namespace KittyFarm
{
    public class DialogController
    {
        public DialogContentDataSO Data
        {
            get => data;
            set
            {
                data = value;
                index = -1;
            }
        }

        private DialogContentDataSO data;

        private int index;

        public bool TryGetNext(out string nextContent)
        {
            index++;
            if (Data == null || index >= Data.Content.Count)
            {
                nextContent = string.Empty;
                return false;
            }

            nextContent = Data.Content[index];
            return true;
        }
    }
}