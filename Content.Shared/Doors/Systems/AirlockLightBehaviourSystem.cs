// using Content.Shared.Doors.Components;
// using Content.Server.Light.Components;
// using Robust.Shared.Maths;

// namespace Content.Shared.Doors.Systems
// {
//     /// <summary>
//     /// Система управления поведением света шлюза.
//     /// </summary>
//     public sealed class AirlockLightBehaviourSystem : EntitySystem
//     {
//         [Dependency] private readonly SharedPointLightSystem _lights = default!;

//         public override void Initialize()
//         {
//             base.Initialize();
//             // Подписка на события или инициализация дополнительных функций (если нужно)
//         }

//         /// <summary>
//         /// Обновление света шлюза в зависимости от состояния.
//         /// </summary>
//         private void UpdateAirlockLight(EntityUid uid, AirlockComponent airlock)
//         {
//             if (!_lights.TryGetLight(uid, out var light))
//                 return;

//             // Проверяем наличие DoorComponent для состояния болтирования
//             if (TryComp<DoorBoltComponent>(uid, out var door))
//             {
//                 _lights.SetColor(uid, Color.FromHex("#dd6756"), light); // Красный для болтирования
//             }
//             else if (airlock.EmergencyAccess)
//             {
//                 _lights.SetColor(uid, Color.FromHex("#ffbf66"), light); // Желтый для аварийного режима
//             }
//             else
//             {
//                 _lights.SetColor(uid, Color.FromHex("#42bfe8"), light); // Голубой для стандартного состояния
//             }
//         }
//     }
// }
