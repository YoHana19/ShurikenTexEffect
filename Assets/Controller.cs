using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Controller : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] ParticleSystemForceField ff;
    [SerializeField] RawImage image;
    [SerializeField] Texture2D runa;
    [SerializeField] Texture2D pinky;
    [SerializeField] Texture2D santori;
    [SerializeField] Texture2D miraiakari;
    [SerializeField] Texture2D yuni;
    [SerializeField, Range(1, 20)] private float power;
    [SerializeField, Range(0.0f, 1.0f)] private float time;

    private ParticleSystem.NoiseModule noiseModule;

    // Start is called before the first frame update
    void Start()
    {
        noiseModule = ps.noise;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*
            ps.gameObject.SetActive(true);
            image.DOFade(0, 2.0f).OnComplete(() => {
                UpNoise().OnComplete(() => {
                    ChangeTexture(pinky);
                    DownNoise().OnComplete(() => {
                        ParticleSystem.MainModule mm = ps.main;
                        mm.loop = false;
                        ParticleSystem.EmissionModule em = ps.emission;
                        em.rateOverTime = 0;
                        image.texture = pinky;
                        image.DOFade(1, 5.0f);
                    });
                });
            });
            */

            noiseModule.strength = 2.5f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeVTuber(runa);
            //noiseModule.strength = 0.0f;
            //ff.gravity = 0.2f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeVTuber(pinky);
            //ff.gravity = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeVTuber(miraiakari);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ChangeVTuber(santori);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            ChangeVTuber(yuni);
        }

        if (noiseModule.strength.constant > 0)
        {
            //noiseModule.strength = noiseModule.strength.constant - Time.deltaTime * power;
        }

    }

    public void UpNoise(float value)
    {
        noiseModule.strength = value;
        var a = value;
        DOTween.To(
            () => a,
            num => a = num,
            time,
            0.0f
        ).OnUpdate(() => {
            noiseModule.strength = a;
        });
    }

    private void ChangeVTuber(Texture2D texture)
    {
        UpNoise().OnComplete(() =>
        {
            ChangeTexture(texture);
            DownNoise();
        });
    }

    private Tween UpNoise()
    {
        ParticleSystem.NoiseModule nm = ps.noise;
        var value = 0.0f;
        return DOTween.To(
            () => value,          
            num => value = num,   
            2.5f,                  
            3.0f                  
        ).OnUpdate(() => {
            nm.strength = value;
        });
    }

    private Tween DownNoise()
    {
        ParticleSystem.NoiseModule nm = ps.noise;
        nm.strength = 0;
        ff.gravity = 0.3f;
        var value = ff.gravity.constant;
        return DOVirtual.DelayedCall(0.5f, () =>
        {
            ff.gravity = 0.0f;
        });
        /*
        return DOTween.To(
            () => value,
            num => value = num,
            0.0f,
            1.0f
        ).OnUpdate(() => {
            ff.gravity = value;
        });
        */
    }

    private void ChangeTexture(Texture2D tex)
    {
        ParticleSystem.ShapeModule sm = ps.shape;
        sm.texture = tex;
    }
}
