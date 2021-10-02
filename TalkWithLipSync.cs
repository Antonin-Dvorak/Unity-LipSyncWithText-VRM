using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

public class TalkWithLipSync : MonoBehaviour
{
    private VRMBlendShapeProxy _proxy;

    [Range(0, 1)] public float _A_Value;
    [Range(0, 1)] public float _I_Value;
    [Range(0, 1)] public float _U_Value;
    [Range(0, 1)] public float _E_Value;
    [Range(0, 1)] public float _O_Value;


    [SerializeField] string message;

    List<char> listA = new List<char>();
    List<char> listI = new List<char>();
    List<char> listU = new List<char>();
    List<char> listE = new List<char>();
    List<char> listO = new List<char>();


    bool isRunning = false;

    IEnumerator coroutineMethod;

    int n = 0;
    string oldVowel;
    string newVowel;

    float valueToIncrease;
    bool changing = false;

    void Start()
    {
        coroutineMethod = DoLipSync();

        _proxy = GetComponent<VRMBlendShapeProxy>();

        char[] charA = new char[] { 'あ', 'か', 'が', 'さ', 'ざ', 'た', 'だ', 'な', 'は', 'ば', 'ま', 'や', 'ら', 'わ' };
        char[] charI = new char[] { 'い', 'き', 'ぎ', 'し', 'じ', 'ち', 'ぢ', 'に', 'ひ', 'び', 'み', 'り' };
        char[] charU = new char[] { 'う', 'く', 'ぐ', 'す', 'ず', 'つ', 'づ', 'ぬ', 'ふ', 'ぶ', 'む', 'ゆ', 'る', 'を'};
        char[] charE = new char[] { 'え', 'け', 'げ', 'せ', 'ぜ', 'て', 'で', 'ね', 'へ', 'べ', 'め', 'れ'};
        char[] charO = new char[] { 'お', 'こ', 'ご', 'そ', 'ぞ', 'と', 'ど', 'の', 'ほ', 'ぼ', 'も', 'よ', 'ろ'};//「ん」はその他

        listA.AddRange(charA);
        listI.AddRange(charI);
        listU.AddRange(charU);
        listE.AddRange(charE);
        listO.AddRange(charO);

        StartCoroutine(coroutineMethod);
    }

    void Update()
    {



        _proxy.SetValues(new Dictionary<BlendShapeKey, float>
                {
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.A), _A_Value}, //_A_Valueの値にブレンドシェイプを持ってくる
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.I), _I_Value}, // [0, 1] の範囲で Weight を指定
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.U), _U_Value}, // [0, 1] の範囲で Weight を指定
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.E), _E_Value}, // [0, 1] の範囲で Weight を指定
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.O), _O_Value}, // [0, 1] の範囲で Weight を指定
                });
        if ((isRunning != null) && (isRunning))//コルーチン中で
        {
            if (changing)//読み込んだ文字の数値が1f以下なら
            {
                
                Count();
                ChangeValue();
            }
            else//１になったら
            {
                //

            }

        }

    }

    IEnumerator DoLipSync()
    {
        if (isRunning) { yield break; }
        isRunning = true;

        foreach (char c in message)//メッセージを一文字ずつ「ｃ」と置いて処理
        {
            if (listA.Contains(c))
            {
                Debug.Log("A");
                newVowel = "A";

            }
            else if (listI.Contains(c))
            {
                Debug.Log("I");
                newVowel = "I";
            }
            else if (listU.Contains(c))
            {
                Debug.Log("U");
                newVowel = "U";
            }
            else if (listE.Contains(c))
            {
                Debug.Log("E");
                newVowel = "E";
            }
            else if (listO.Contains(c))
            {
                Debug.Log("O");
                newVowel = "O";
            }
            changing = true;
            //
            Debug.Log("Stop!");
            
            yield return new WaitForSeconds(0.2f);
        }
        
        isRunning = false;
    }

    void Count()
    {
        if (n == 0)
        {
            //StopCoroutine(coroutineMethod);
        }

        n += 1;
        //Debug.Log(n);
        if (n == 10)//同音が続いても問題ない
        {
            changing = false;
            n = 0;
            oldVowel = newVowel;
            newVowel = null;
            Debug.Log(oldVowel);
            //StartCoroutine(coroutineMethod);
        }
    }




    void ChangeValue()
    {
        if (oldVowel != null)
        {
            if (oldVowel == "A")
            {
                if (_A_Value >= 0f)
                {
                    _A_Value -= 0.1f;
                }
            }
            else if (oldVowel == "I")
            {
                if (_I_Value >= 0f)
                {
                    _I_Value -= 0.1f;
                }
            }
            else if (oldVowel == "U")
            {
                if (_U_Value >= 0f)
                {
                    _U_Value -= 0.1f;
                }
            }
            else if (oldVowel == "E")
            {
                if (_E_Value >= 0f)
                {
                    _E_Value -= 0.1f;
                }
            }
            else if (oldVowel == "O")
            {
                if (_O_Value >= 0f)
                {
                    _O_Value -= 0.1f;
                }
            }
        }
        //valueToCrease += 0.1f;
        if (newVowel != null)
        {
            if (newVowel == "A")
            {
                if (_A_Value <= 1f)
                {
                    _A_Value += 0.1f;

                }
            }
            else if (newVowel == "I")
            {
                if (_I_Value <= 1f)
                {
                    _I_Value += 0.1f;
                }
            }
            else if (newVowel == "U")
            {
                if (_U_Value <= 1f)
                {
                    _U_Value += 0.1f;
                }
            }
            else if (newVowel == "E")
            {
                if (_E_Value <= 1f)
                {
                    _E_Value += 0.1f;
                }
            }
            else if (newVowel == "O")
            {
                if (_O_Value <= 1f)
                {
                    _O_Value += 0.1f;
                }
            }
        }
    }




    void ResetValue()
    {
        

        _A_Value = 0;
        _I_Value = 0;
        _U_Value = 0;
        _E_Value = 0;
        _O_Value = 0;
    }

}