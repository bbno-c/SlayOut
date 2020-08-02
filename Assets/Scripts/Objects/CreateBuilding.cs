using UnityEngine;
using System.Collections;

namespace Objects
{
    public abstract class CreateBuilding : Ability
    {
        public int Radius = 5;
        public Building BuildingPrefab;
    }
}