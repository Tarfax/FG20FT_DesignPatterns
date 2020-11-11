using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace DesignPatternCourse.FactoryUsingReflection
{
    public static class AbilityFactory
    {
        static Dictionary<string, Type> abilitiesByName;

        static void Init()
        {
            if (abilitiesByName != null)
                return;

            IEnumerable<Type> abilityTypes = Assembly.GetAssembly(typeof(Ability)).GetTypes().Where(
                myType => myType.IsClass && myType.IsAbstract == false && myType.IsSubclassOf(typeof(Ability)));

            abilitiesByName = new Dictionary<string, Type>();

            foreach(Type type in abilityTypes)
            {
                Ability ability = Activator.CreateInstance(type) as Ability;
                abilitiesByName.Add(ability.Name, type);
            }
        }

        public static Ability GetAbility(string name)
        {
            Init();

            if(abilitiesByName.ContainsKey(name) == false)
            {
                Debug.LogError("Ability name not found!");
                return null;
            }

            Type type = abilitiesByName[name];
            Ability ability = Activator.CreateInstance(type) as Ability;
            return ability;
        }

        public static IEnumerable<string> GetAbilityNames()
        {
            Init();
            return abilitiesByName.Keys;
        }
    }
}