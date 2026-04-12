using Assets.Game.Scripts.Buildings;
using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace Assets.Game.Scripts.Animations
{
    public class BuildingAnimations : IBuildingAnimations
    {
        public IEnumerator PlayBuildingAppearanceAnimation(Building building)
        {
            var buildingGroundPosition = building.transform.position;

            building.transform.position += new Vector3(0, 10, 0);

            yield return building.transform.DOMove(buildingGroundPosition, 0.5f)
                .SetEase(Ease.InExpo).WaitForCompletion();

            building.transform.DOScaleX(1.2f, 0.15f)
                .SetEase(Ease.OutBack);

            building.transform.DOScaleZ(1.2f, 0.15f)
                .SetEase(Ease.OutBack);

            yield return building.transform.DOScaleY(0.1f, 0.15f)
                .SetEase(Ease.OutBack).WaitForCompletion();

            yield return building.transform.DOScale(1.0f, 0.15f)
                .SetEase(Ease.OutBack).WaitForCompletion();
        }
    }

    public interface IBuildingAnimations
    {
        IEnumerator PlayBuildingAppearanceAnimation(Building building);
    }
}