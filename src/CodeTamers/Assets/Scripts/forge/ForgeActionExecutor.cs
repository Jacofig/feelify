public class ForgeActionExecutor
{
    public bool Execute(ForgeAction action, ForgeProcess process)
    {
        var metal = process.metal;

        switch (action.Type)
        {
            case ForgeActionType.Heat:
                metal.temperature += 50;
                process.RegisterAction(action);
                return true;

            case ForgeActionType.Hit:
                if (metal.temperature < metal.minHitTemp)
                {
                    process.Fail("Hit on cold metal");
                    return false;
                }

                metal.hits++;
                metal.temperature -= metal.minHitTemp; 

                if (metal.temperature < 0)
                    metal.temperature = 0;

                process.RegisterAction(action);
                return true;


            case ForgeActionType.Add:
                if (metal.hits == 0)
                {
                    process.Fail("Add before shaping");
                    return false;
                }
                metal.additives.Add(action.Argument);
                process.RegisterAction(action);
                return true;

            case ForgeActionType.Cast:
                if (metal.enchanted)
                {
                    process.Fail("Already enchanted");
                    return false;
                }

                metal.enchanted = true;
                process.RegisterAction(action);
                return true;


        }

        return false;
    }
}
