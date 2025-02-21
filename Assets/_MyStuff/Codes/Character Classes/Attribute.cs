using UnityEngine;
using System.Collections;

public class Attribute {
    public enum AttributeType { Primary, Physical, Ancillary }
    public string name;
    public string description;
    public AttributeType type;
    public int ID;
    public Tuple<int, int> attributeRange;
    public int defaultValue;
    public bool active;
    //--- Primary Attribute
    public int fullPhysicalEffect;
    public int[] halfPhysicalEffect;
    //--- Physical Attribute
    public int[] primaryEffects;
    public int[] ancillaryEffects;
    //--- Ancillary Attribute
    public int[] parentedPhysicalAttributes;
    public int parentedPhysicalAttribute;
    public int linkedSpecialization;
    public int attributeRatio;
    
    public Attribute(AttributeType attributeType, string attributeName, string attributeDescription, string[] entries) {
        active = true;
        name = attributeName;
        description = attributeDescription;
        type = attributeType;
        int min, max;
        switch (attributeType) {
            case AttributeType.Primary:
                //Parse physical attribute parents of primary attribute
                int.TryParse(entries[2], out fullPhysicalEffect);
                string[] halfPEffects = entries[3].Split(':');
                halfPhysicalEffect = new int[halfPEffects.Length];
                for (int i = 0; i < halfPEffects.Length; i++) {
                    int.TryParse(halfPEffects[i], out halfPhysicalEffect[i]);
                }
                //Parse range
                string[] pAttRange = entries[4].Split(':');
                if (pAttRange.Length == 2) {
                    int.TryParse(pAttRange[0], out min);
                    int.TryParse(pAttRange[1], out max);
                    attributeRange = new Tuple<int, int>(min, max);
                }
                //Parse default value
                int.TryParse(entries[5], out defaultValue);
                //Parse ID
                int.TryParse(entries[6], out ID); 
                break;
            case AttributeType.Physical:
                //Parse primary effects
                string[] pEffects = entries[2].Split(':');
                primaryEffects = new int[pEffects.Length];
                for (int i = 0; i < primaryEffects.Length; i++) {
                    int.TryParse(pEffects[i], out primaryEffects[i]);
                }
                //Parse ancillary effects
                string[] aEffects = entries[3].Split(':');
                ancillaryEffects = new int[aEffects.Length];
                for (int i = 0; i < ancillaryEffects.Length; i++) {
                    int.TryParse(aEffects[i], out ancillaryEffects[i]);
                }
                //Parse range
                string[] phAttRange = entries[4].Split(':');
                if (phAttRange.Length == 2) {
                    int.TryParse(phAttRange[0], out min);
                    int.TryParse(phAttRange[1], out max);
                    attributeRange = new Tuple<int, int>(min, max);
                }
                //Parse default value
                int.TryParse(entries[5], out defaultValue);
                //Parse ID
                int.TryParse(entries[6], out ID);
                break;
            case AttributeType.Ancillary:
                int.TryParse(entries[2], out parentedPhysicalAttribute);
                int.TryParse(entries[3], out attributeRatio);
                string[] aAttRange = entries[4].Split(':');
                if (aAttRange.Length == 2) {
                    int.TryParse(aAttRange[0], out min);
                    int.TryParse(aAttRange[1], out max);
                    attributeRange = new Tuple<int, int>(min, max);
                }
                int.TryParse(entries[5], out defaultValue);
                int.TryParse(entries[6], out ID);
                break;
        }
    }
}
