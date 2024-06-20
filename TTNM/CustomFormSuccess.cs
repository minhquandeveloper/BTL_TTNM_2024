using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTNM
{
    public static class CustomFormSuccess
    {
        public static void ShowSuccess(string message)
        {
            using (var successForm = new CustomForm(message))
            {
                successForm.ShowDialog();
            }
        }
    }
}
