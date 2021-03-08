using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class BreathingColor : MonoBehaviour
{
    // UNI
    private GameObject uni;

    // Beathing mecanic
    private Breathing_mechanic bm;

    private Volume vol;

    private Vignette vig;
    private ChromaticAberration chrom;
    private ColorAdjustments colorAdjust;
    private FilmGrain grain;
    private ColorCurves colorCurve;

    public int step1 = 75;
    public int step2 = 50;
    public int step3 = 25;
    public int step4 = 10;

    // Start is called before the first frame update
    void Start()
    {

        // Get uni
        uni = GameObject.FindGameObjectWithTag("uni");

        // Get uni
        bm = uni.GetComponent<Breathing_mechanic>();

        // Get component Volume to change
        vol = GetComponent<Volume>();


        Vignette tmpvig;
        if (vol.profile.TryGet<Vignette>(out tmpvig))
        {
            vig = tmpvig;
        }


        ChromaticAberration tmpchrom;
        if (vol.profile.TryGet<ChromaticAberration>(out tmpchrom))
        {
            chrom = tmpchrom;
        }


        ColorAdjustments tmpcolorAdjust;
        if (vol.profile.TryGet<ColorAdjustments>(out tmpcolorAdjust))
        {
            colorAdjust = tmpcolorAdjust;
        }


        FilmGrain tmpgrain;
        if (vol.profile.TryGet<FilmGrain>(out tmpgrain))
        {
            grain = tmpgrain;
        }

        ColorCurves tmpcolorCurve;
        if (vol.profile.TryGet<ColorCurves>(out tmpcolorCurve))
        {
            colorCurve = tmpcolorCurve;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (bm.breath< step1 && bm.breath>=0)
        {
            if (chrom)
                chrom.intensity.value = Mathf.Abs(bm.breath - step1) / step1;

            if (bm.breath < step2)
            {

                if (vig)
                    vig.intensity.value = (Mathf.Abs(bm.breath - step2) / step2) * 0.5f;

                if (bm.breath < step3)
                {

                    if (colorAdjust)
                        colorAdjust.saturation.value = (bm.breath*100/step3) - 100;

                    if (grain)
                        grain.intensity.value = Mathf.Abs(bm.breath - step3) / step3;

                    if (bm.breath < step4)
                    {

                        colorCurve.active = true;
                    }

                }

            }

        }
        else if (bm.breath >= 0)
        {
            if (chrom)
                chrom.intensity.value = 0;

            if (vig)
                vig.intensity.value = 0;

            if (colorAdjust)
                colorAdjust.saturation.value = 0;

            if (grain)
                grain.intensity.value = 0;

            colorCurve.active = false;
        }







        /*
                GetComponent<ChromaticAberration>().intensity.value = (100 / bm.breath) / 100;
                GetComponent<ColorAdjustments>().saturation.value = bm.breath - 100;
                GetComponent<FilmGrain>().intensity.value = (100 / bm.breath) / 100;
        */
    }
}
