using Content.Server.Stunnable.Components;
using Content.Shared.Standing;
using Content.Shared.StatusEffect;
using JetBrains.Annotations;
using Robust.Shared.Physics.Dynamics;
using Content.Shared.Throwing;
using Robust.Shared.Physics.Events;

namespace Content.Server.Stunnable
{
    [UsedImplicitly]
    internal sealed class StunOnCollideFloatSystem : EntitySystem
    {
        [Dependency] private readonly StunSystem _stunSystem = default!;

        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<StunOnCollideFloatComponent, StartCollideEvent>(HandleCollide);
            SubscribeLocalEvent<StunOnCollideFloatComponent, ThrowDoHitEvent>(HandleThrow);
        }

        private void TryDoCollideStun(EntityUid uid, StunOnCollideFloatComponent component, EntityUid target)
        {

            if (EntityManager.TryGetComponent<StatusEffectsComponent>(target, out var status))
            {
                _stunSystem.TryStun(target, TimeSpan.FromSeconds(component.StunAmount), true, status);

                _stunSystem.TryKnockdown(target, TimeSpan.FromSeconds(component.KnockdownAmount), true,
                    status);

                _stunSystem.TrySlowdown(target, TimeSpan.FromSeconds(component.SlowdownAmount), true,
                    component.WalkSpeedMultiplier, component.RunSpeedMultiplier, status);
            }
        }
        private void HandleCollide(EntityUid uid, StunOnCollideFloatComponent component, ref StartCollideEvent args)
        {
            if (args.OurFixtureId != component.FixtureID)
                return;

            TryDoCollideStun(uid, component, args.OtherEntity);
        }

        private void HandleThrow(EntityUid uid, StunOnCollideFloatComponent component, ThrowDoHitEvent args)
        {
            TryDoCollideStun(uid, component, args.Target);
        }
    }
}