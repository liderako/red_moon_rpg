using Entitas;

/*
** Харизма влияет на способность персонажа
** 1. Убеждать людей с помощью риторики.
** 2. Торговаться
*/

[Game]
public class PersonalityComponent : IComponent
{
    public int value;
}
