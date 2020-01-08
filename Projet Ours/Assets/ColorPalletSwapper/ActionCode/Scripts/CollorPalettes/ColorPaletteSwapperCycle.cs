using UnityEngine;

namespace ActionCode.ColorPalettes
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ColorPaletteSwapper))]
    public sealed class ColorPaletteSwapperCycle : MonoBehaviour
    {
        public KeyCode swapKey = KeyCode.N;
        public ColorPaletteSwapper swapper;
        public ColorPalette[] palettes;
        public GameObject[] rages;

        private int _palletIndex = -1;


        private void Reset()
        {
            swapper = GetComponent<ColorPaletteSwapper>();
        }

        private void Update()
        {
            //if (Input.GetKeyDown(swapKey)) SwapPalette();

            if(rages[0].activeSelf == true)
            {
                swapper.SwitchPalette(palettes[0]);
            }

            if (rages[1].activeSelf == true)
            {
                swapper.SwitchPalette(palettes[1]);
            }

            if (rages[2].activeSelf == true)
            {
                swapper.SwitchPalette(palettes[2]);
            }

        }

        /*private void SwapPalette()
        {
            if (palettes.Length == 0) return;

            _palletIndex = (_palletIndex + 1) % palettes.Length;
            swapper.SwitchPalette(palettes[_palletIndex]);
        }*/
    }
}