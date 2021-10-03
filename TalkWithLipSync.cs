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

        char[] charA = new char[] { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
        char[] charI = new char[] { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
        char[] charU = new char[] { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
        char[] charE = new char[] { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
        char[] charO = new char[] { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };//�u��v�͂��̑�

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
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.A), _A_Value}, //_A_Value�̒l�Ƀu�����h�V�F�C�v�������Ă���
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.I), _I_Value}, // [0, 1] �͈̔͂� Weight ���w��
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.U), _U_Value}, // [0, 1] �͈̔͂� Weight ���w��
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.E), _E_Value}, // [0, 1] �͈̔͂� Weight ���w��
                    {BlendShapeKey.CreateFromPreset(BlendShapePreset.O), _O_Value}, // [0, 1] �͈̔͂� Weight ���w��
                });
        if ((isRunning != null) && (isRunning))//�R���[�`������
        {
            if (changing)//�ǂݍ��񂾕����̐��l��1f�ȉ��Ȃ�
            {
                ChangeValue();
            }
        }
    }

    IEnumerator DoLipSync()
    {
        if (isRunning) { yield break; }
        isRunning = true;

        foreach (char c in message)//���b�Z�[�W���ꕶ�����u���v�ƒu���ď���
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
            yield return new WaitForSeconds(0.1f);
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
                if (oldVowel == "A")
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
        else if (n == 10)
        {
            changing = false;
            n = 0;
            oldVowel = newVowel;
            newVowel = null;
            Debug.Log(oldVowel);
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