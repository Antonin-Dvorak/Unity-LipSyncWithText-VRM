using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

public class PresetLipSync : MonoBehaviour
{
    private VRMBlendShapeProxy _proxy;

    [Range(0, 1)] public float _A_Value;
    [Range(0, 1)] public float _I_Value;
    [Range(0, 1)] public float _U_Value;
    [Range(0, 1)] public float _E_Value;
    [Range(0, 1)] public float _O_Value;
    [Range(0, 1)] public float _Blink_Value;
    [Range(0, 1)] public float _Fun_Value;



    [SerializeField] string message;

    List<char> listA = new List<char>();
    List<char> listI = new List<char>();
    List<char> listU = new List<char>();
    List<char> listE = new List<char>();
    List<char> listO = new List<char>();
    List<char> listM = new List<char>();
    List<char> listBlink = new List<char>();
    List<char> listFun = new List<char>();

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

        char[] charA = new char[] { 'あ', 'ぁ', 'か', 'が', 'さ', 'ざ', 'た', 'だ', 'な', 'は', 'ば', 'や', 'ら', 'わ' };
        char[] charI = new char[] { 'い', 'ぃ', 'き', 'ぎ', 'し', 'じ', 'ち', 'ぢ', 'に', 'ひ', 'び', 'り' };
        char[] charU = new char[] { 'う', 'ぅ', 'く', 'ぐ', 'す', 'ず', 'つ', 'づ', 'ぬ', 'ふ', 'ぶ', 'ゆ', 'る', 'を'};
        char[] charE = new char[] { 'え', 'ぇ', 'け', 'げ', 'せ', 'ぜ', 'て', 'で', 'ね', 'へ', 'べ', 'れ'};
        char[] charO = new char[] { 'お', 'ぉ', 'こ', 'ご', 'そ', 'ぞ', 'と', 'ど', 'の', 'ほ', 'ぼ', 'よ', 'ろ'};//「ん」はその他
        char[] charM = new char[] { 'ま' ,'み', 'む', 'め', 'も', 'ぱ', 'ぴ', 'ぷ', 'ぺ', 'ぽ', 'ば', 'び', 'ぶ', 'べ', 'ぼ' };
        char[] charBlink = new char[] { '瞬'};
        char[] charFun = new char[] { '笑'};


        listA.AddRange(charA);
        listI.AddRange(charI);
        listU.AddRange(charU);
        listE.AddRange(charE);
        listO.AddRange(charO);
        listM.AddRange(charM);
        listBlink.AddRange(charBlink);
        listFun.AddRange(charFun);

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
                
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.Blink), _Blink_Value}, // [0, 1] の範囲で Weight を指定
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.Fun), _Fun_Value}, // [0, 1] の範囲で Weight を指定
        });
        if ((isRunning != null) && (isRunning))//コルーチン中で
        {
            if (changing)//読み込んだ文字の数値が1f以下なら
            { 
                ChangeValue();
            }
        }
    }

    IEnumerator DoLipSync()
    {
        if (isRunning) { yield break; }
        isRunning = true;

        foreach (char c in message)//メッセージを一文字ずつ「ｃ」と置いて処理
        {
            if (listBlink.Contains(c))
            {
                _Blink_Value = 1;
            }
            if (listFun.Contains(c))
            {
                _Fun_Value = 0.5f;
            }


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
            else if (listM.Contains(c))
            {
                Debug.Log("M");
                newVowel = "M";
            }
            changing = true;
            yield return new WaitForSeconds(1f);
        }
        
        isRunning = false;
    }

    void ChangeValue()
    {
        StopCoroutine(coroutineMethod);
        n += 1;
        if (n != 10)
        {
            if (oldVowel != null)
            {
                if ((oldVowel == "A") | (oldVowel == "M"))
                {
                    _A_Value -= 0.1f;
                }
                else if (oldVowel == "I")
                {
                    _I_Value -= 0.1f;
                }
                else if (oldVowel == "U")
                {
                    _U_Value -= 0.1f;
                }
                else if (oldVowel == "E")
                {
                    _E_Value -= 0.1f;
                }
                else if (oldVowel == "O")
                {
                    _O_Value -= 0.1f;
                }
            }
            if (newVowel != null)
            {
                if (newVowel == "A")
                {
                        _A_Value += 0.1f;
                }
                else if (newVowel == "I")
                {
                        _I_Value += 0.1f;
                }
                else if (newVowel == "U")
                {
                        _U_Value += 0.1f;
                }
                else if (newVowel == "E")
                {
                        _E_Value += 0.1f;
                }
                else if (newVowel == "O")
                {
                        _O_Value += 0.1f;
                }
                else if (newVowel == "M")
                {
                    if (n == 1)
                    {
                        oldVowel = null;
                        ResetValue();
                    }
                    _A_Value += 0.1f;
                }
            }
        }
        else if (n == 10)
        {
            changing = false;
            n = 0;
            oldVowel = newVowel;
            newVowel = null;
            StartCoroutine(coroutineMethod);
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
