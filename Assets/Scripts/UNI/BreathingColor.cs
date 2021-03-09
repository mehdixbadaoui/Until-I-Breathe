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
    private WhiteBalance whitebalance;

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

        WhiteBalance tmpwhitebalance;
        if (vol.profile.TryGet<WhiteBalance>(out tmpwhitebalance))
        {
            whitebalance = tmpwhitebalance;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (bm.breath< step1 && bm.breath>=0)
        {
            if (chrom && bm.breath > step2 - (bm.max_breath/10))
                chrom.intensity.value = ( Mathf.Abs(bm.breath - step1) / step1 ) * (bm.max_breath / 10);

            if (bm.breath < step2)
            {

                if (vig && bm.breath > step2 - (bm.max_breath / 10))
                    vig.intensity.value = (Mathf.Abs(bm.breath - step2) / step2) * (bm.max_breath / (bm.max_breath / 10)) * 0.25f;

                if (bm.breath < step3)
                {


                    if (grain && bm.breath > step3 - (bm.max_breath / 10))
                        grain.intensity.value = (Mathf.Abs(bm.breath - step3) / step3) * (bm.max_breath / 10);

                    if (whitebalance && bm.breath > step3 - (bm.max_breath / 10))
                    {
                        whitebalance.temperature.value = ( Mathf.Abs(bm.breath - step3) / step3 ) * bm.max_breath * (bm.max_breath / 10);
                        whitebalance.tint.value = (Mathf.Abs(bm.breath - step3) / step3) * bm.max_breath * (bm.max_breath / (bm.max_breath / 10));
                    }

                    if (bm.breath < step4 )
                    {
                        if (colorAdjust)
                            colorAdjust.saturation.value = (bm.breath * bm.max_breath / step4) - bm.max_breath;
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

            if (whitebalance)
            {
                whitebalance.temperature.value = 0;
                whitebalance.tint.value = 0;
            }
        }







        /*
                GetComponent<ChromaticAberration>().intensity.value = (100 / bm.breath) / 100;
                GetComponent<ColorAdjustments>().saturation.value = bm.breath - 100;
                GetComponent<FilmGrain>().intensity.value = (100 / bm.breath) / 100;
        */
    }
}
