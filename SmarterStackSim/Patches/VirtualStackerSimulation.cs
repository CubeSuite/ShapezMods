using Game.Core.Simulation;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmarterStackSim.Patches
{
    internal class VirtualStackerSimulationPatch
    {
        [HarmonyPatch(typeof(VirtualStackerSimulation), nameof(VirtualStackerSimulation.Update))]
        [HarmonyPostfix] // Delete one
        static void PatchName(VirtualStackerSimulation __instance, Ticks startTicks_T, Ticks deltaTicks_T) {
            int amountOfSignalsThisUdpate = SignalSimulation.GetAmountOfSignalsThisUpdate(startTicks_T, deltaTicks_T);
            SignalTick signalTick = SignalTick.FromTicks(startTicks_T);
            
            for (int i = 0; i < amountOfSignalsThisUdpate; i++) {
                SignalTick signalTick2 = new SignalTick(signalTick.Value + (long)i);
                ISignalValue signalValue;
                __instance.Input0Conductor.TryPopValue(startTicks_T, signalTick2, out signalValue);
                ISignalValue signalValue2;
                __instance.Input1Conductor.TryPopValue(startTicks_T, signalTick2, out signalValue2);
                ISignalValue signalValue3 = SignalNullValue.Instance;

                ShapeDefinition shapeDefinition;
                ShapeDefinition shapeDefinition2;
                bool signal1IsShape = signalValue.TryConvertToShape(out shapeDefinition);
                bool signal2IsShape = signalValue2.TryConvertToShape(out shapeDefinition2);

                if (signal1IsShape == signal2IsShape) return;
                if (signal1IsShape) signalValue3 = signalValue;
                if (signal2IsShape) signalValue3 = signalValue2;

                __instance.OutputConductor.PushValue(signalValue3, startTicks_T, signalTick2);
            }
        }
    }
}
