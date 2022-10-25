using Entitas;
using Entitas.CodeGeneration.Attributes;

/*
 * Компонент используется для прикрипление данного компонента к конкретному игровому юниту
 * Чтобы понимать к какой фракции тот или иной игровой юнит относится
 */
[Battle]
public class TypeFactionComponent : IComponent
{
    [EntityIndex]
    public Factions value;
}
