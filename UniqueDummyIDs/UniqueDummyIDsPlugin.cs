using HarmonyLib;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace UniqueDummyIDs;

public class UniqueDummyIDsPlugin : Plugin
{
    public override string Name { get; } = "Unique Dummy IDs";
    public override string Description { get; } = "Dummys are given unique IDs when spawned instead of the same 'ID_Dummy' ID.";
    public override string Author { get; } = "acecrum";
    public override Version Version { get; } = new Version(1, 0, 0);
    public override Version RequiredApiVersion { get; } = new (LabApiProperties.CompiledVersion);

    private Harmony? _harmony;
    
    public override void Enable()
    {
        _harmony = new Harmony("acecrum.uniquedummyids");
        _harmony.PatchAll();
    }

    public override void Disable()
    {
        _harmony?.UnpatchAll();
    }
}