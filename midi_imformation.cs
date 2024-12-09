using Klak.Timeline.Midi;
using UnityEngine;

namespace Logger
{
    public class midi_imfomation : MonoBehaviour
    {
        [SerializeField] private MidiAnimationAsset _asset;
        int[] data_43 = new int[500];
        int[] data_38 = new int[500];
        int[] data_36 = new int[500];
        private void Start()
        {
            var midiEventSet = _asset.template.events;
            int count_43 = 0;
            int count_38 = 0;
            int count_36 = 0;
            foreach (MidiEvent midiEvent in midiEventSet)
            {
                
                if (midiEvent.status == 144)
                {
                    if (midiEvent.data1 == 43)
                    {
                        data_43[count_43] = (int)midiEvent.time;
                        count_43++;
                        continue;
                    }
                    if (midiEvent.data1 == 38)
                    {
                        data_38[count_38] = (int)midiEvent.time;
                        count_38++;
                    }
                    if (midiEvent.data1 == 36)
                    {
                        data_36[count_36] = (int)midiEvent.time;
                        count_36++;
                    }
                    //Debug.Log(midiEvent.ToString());
                }
            }
            for (int i = 0;i<count_43;i++)
            Debug.Log("data_43 "+data_43[i]);
            for (int i = 0; i<count_38;i++)
            Debug.Log("data_38 "+data_38[i]);
            for(int i = 0;i<count_36;i++)
            Debug.Log("data_36 "+data_36[i]);
        }
    }
}
