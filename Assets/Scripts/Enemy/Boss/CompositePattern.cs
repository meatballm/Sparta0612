using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositePattern : IBossPattern
{
    private List<IBossPattern> patterns;

    public CompositePattern(params IBossPattern[] patterns)
    {
        this.patterns = new List<IBossPattern>(patterns);
    }

    public void Execute(BossController boss)
    {
        foreach (var pattern in patterns)
        {
            pattern.Execute(boss);
        }
    }
}
