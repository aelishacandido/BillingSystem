using System;
using System.Collections.Generic;
using System.Text;

namespace BillingSystem.Utils
{
    /// <summary> 
    /// Defines the standard color palette used across 
    /// all forms in the Billing System. 
    /// Change a color here to update it everywhere. 
    /// </summary> 
    public static class AppTheme
    {
        // Primary deep blue — form headers, main buttons 
        public static Color PrimaryColor = Color.FromArgb(31, 78, 121);

        // Medium blue — sub-headers, active panels 
        public static Color SecondaryColor = Color.FromArgb(46, 117, 182);

        // Light blue — DataGridView headers, light backgrounds 
        public static Color AccentColor = Color.FromArgb(189, 215, 238);

        // Red — delete buttons, danger actions 
        public static Color DangerColor = Color.FromArgb(192, 0, 0);

        // Dark green — save/add buttons, success actions 
        public static Color SuccessColor = Color.FromArgb(55, 86, 35);

        // White — form backgrounds 
        public static Color BackgroundColor = Color.White;

        // Dark grey — all regular body text 
        public static Color TextColor = Color.FromArgb(64, 64, 64);

        // Light grey — alternating DataGridView rows 
        public static Color GridRowAlt = Color.FromArgb(240, 245, 255);

        // Light green — active customer rows 
        public static Color ActiveRowColor = Color.FromArgb(198, 224, 180);

        // Light red — inactive customer rows 
        public static Color InactiveRowColor = Color.FromArgb(255, 199, 206);
    }
}
