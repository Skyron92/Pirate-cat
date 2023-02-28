using UnityEngine;

public class Cat : MonoBehaviour {
    public int NavigateurStat;
    public int CanonnierStat;
    public int EscrimeurStat;

    public Cat(int navigateurStat, int canonnierStat, int escrimeurStat) {
        NavigateurStat = navigateurStat;
        CanonnierStat = canonnierStat;
        EscrimeurStat = escrimeurStat;
    }
}