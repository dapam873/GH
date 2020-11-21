using System;

using System.Windows.Forms;

namespace howto_sort_list_columns
{
    // Compare deux éléments ListView en fonction d'une colonne sélectionnée.
    public class ListViewComparer : System.Collections.IComparer
    {
        private readonly int ColumnNumber;
        private readonly SortOrder SortOrder;

        public ListViewComparer(int column_number, SortOrder sort_order)
        {
            ColumnNumber = column_number;
            SortOrder = sort_order;
        }

        // Comparez deux ListViewItems.
        public int Compare(object object_x, object object_y)
        {
            // Obtenez les objets en tant que ListViewItems.
            ListViewItem item_x = object_x as ListViewItem;
            ListViewItem item_y = object_y as ListViewItem;

            // Obtenez les valeurs de sous-éléments correspondants.
            string string_x;
            if (item_x.SubItems.Count <= ColumnNumber)
            {
                string_x = "";
            }
            else
            {
                string_x = item_x.SubItems[ColumnNumber].Text;
            }

            string string_y;
            if (item_y.SubItems.Count <= ColumnNumber)
            {
                string_y = "";
            }
            else
            {
                string_y = item_y.SubItems[ColumnNumber].Text;
            }

            // Comparez-les.
            int result;
            if (double.TryParse(string_x, out double double_x) &&
                double.TryParse(string_y, out double double_y))
            {
                // Traiter comme un nombre.
                result = double_x.CompareTo(double_y);
            }
            else
            {
                if (DateTime.TryParse(string_x, out DateTime date_x) &&
                    DateTime.TryParse(string_y, out DateTime date_y))
                {
                    // Traiter comme une date.
                    result = date_x.CompareTo(date_y);
                }
                else
                {
                    // Traiter comme string.
                    result = string_x.CompareTo(string_y);
                }
            }

            // Retourne le résultat correct selon que
            // nous trions par ordre croissant ou décroissant.
            if (SortOrder == SortOrder.Ascending)
            {
                return result;
            }
            else
            {
                return -result;
            }
        }
    }
}
