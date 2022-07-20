using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendManager {
    public HashSet<GameObject> mTurrets = new HashSet<GameObject>();
    public HashSet<GameObject> mFriends = new HashSet<GameObject>();
    public void AddTurret(GameObject turret) {
        mTurrets.Add(turret);
    }
    public void RemoveTurret(GameObject turret) {
        mTurrets.Remove(turret);
    }
    public void ClearTurrets() {
        mTurrets.Clear();
    }
    public void AddFriend(GameObject friend) {
        mFriends.Add(friend);
    }
    public void RemoveFriend(GameObject friend) {
        mFriends.Remove(friend);
    }
    public void ClearFriends() {
        mFriends.Clear();
    }
    static float Dist2(Vector3 a,Vector3 b) {
        return (a.x-b.x)*(a.x-b.x)+(a.y-b.y)*(a.y-b.y);
    }
    public GameObject GetClosestFriend(Vector3 pos) {
        GameObject closest = null;
        float closestDist = float.MaxValue;
        foreach (GameObject friend in mFriends) {
            float dist = Dist2(friend.transform.position,pos);
            if (dist < closestDist) {
                closestDist = dist;
                closest = friend;
            }
        }
        return closest;
    }
    public GameObject GetClosestTurret(Vector3 pos) {
        GameObject closest = null;
        float closestDist = float.MaxValue;
        foreach (GameObject turret in mTurrets) {
            float dist = Dist2(turret.transform.position,pos);
            if (dist < closestDist) {
                closestDist = dist;
                closest = turret;
            }
        }
        return closest;
    }
    public GameObject GetClosest(Vector3 pos) {
        GameObject closest1 = GetClosestTurret(pos);
        GameObject closest2 = GetClosestFriend(pos);
        if (closest1 == null) {
            return closest2;
        }
        if (closest2 == null) {
            return closest1;
        }
        if (Dist2(closest1.transform.position,pos) < Dist2(closest2.transform.position,pos)) {
            return closest1;
        } else {
            return closest2;
        }
    }
    public GameObject GetClosestWithHero(Vector3 pos) {
        GameObject closest = GetClosest(pos);
        GameObject hero = GameManager.sTheGlobalBehavior.mHero.gameObject;
        if (closest == null) {
            return hero;
        }
        if (Dist2(closest.transform.position,pos) < Dist2(hero.transform.position,pos)) {
            return closest;
        } else {
            return hero;
        }
    }
    public GameObject GetClosestT<T>(Vector3 pos) where T : MonoBehaviour {
        GameObject closest = null;
        float closestDist = float.MaxValue;
        foreach (GameObject turret in mTurrets) {
            if (turret.GetComponent<T>() != null) {
                float dist = Dist2(turret.transform.position,pos);
                if (dist < closestDist) {
                    closestDist = dist;
                    closest = turret;
                }
            }
        }
        foreach (GameObject friend in mFriends) {
            if (friend.GetComponent<T>() != null) {
                float dist = Dist2(friend.transform.position,pos);
                if (dist < closestDist) {
                    closestDist = dist;
                    closest = friend;
                }
            }
        }
        return closest;
    }
}