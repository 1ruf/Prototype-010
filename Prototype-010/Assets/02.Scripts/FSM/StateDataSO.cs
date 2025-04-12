using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "StateData", menuName = "SO/FSM/StateData", order = 0)]
public class StateDataSO : ScriptableObject
{
    public string stateName;
    public string className;
    public string animParamName;

    //이 해쉬값은 절대로 private로 하면 안된다
    public int animationHash;


    private void OnValidate() //SO에 변화가 생겼을때 호출 (에디텅서만)
    {
        animationHash = Animator.StringToHash(animParamName);
    }
}