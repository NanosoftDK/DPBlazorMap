namespace DPBlazorMapLibrary
{
    public class TooltipOptions : LayerOptions
    {
        /// <summary>
        /// Whether to open the tooltip permanently or only on mouseover.
        /// </summary>
        public bool Permanent { get; set; }


        /// <summary>
        /// 	Direction where to open the tooltip. Possible values are: right, left, top, bottom, center, auto. auto will dynamically switch between right and left according to the tooltip position on the map.
        /// </summary>
        public string Direction { get; set; } = "auto";

    }
}
