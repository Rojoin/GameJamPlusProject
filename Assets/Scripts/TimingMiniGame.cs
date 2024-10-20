using UnityEngine;

public class TimingMiniGame : HackingMiniGameBase
{
    public RectTransform rotatingSprite;
    public RectTransform objectiveTransform;

    public float rotatingSpeed = 100f;
    public float threshold = 100f;
    private bool isActive = false;
    private float targetRotation = 0;

    [ContextMenu("Start Game")]
    public override void StartMiniGame()
    {
        base.StartMiniGame();

        isActive = true;
        targetRotation = Random.Range(0, 359);
        objectiveTransform.rotation = Quaternion.Euler(0, 0, targetRotation);
        rotatingSprite.rotation = Quaternion.identity;
    }

    public override void UpdateMiniGame()
    {
        if (isActive)
        {
            Vector3 eulerAngles = rotatingSprite.rotation.eulerAngles;

            eulerAngles += new Vector3(0, 0, rotatingSpeed * Time.deltaTime);
            rotatingSprite.rotation = Quaternion.Euler(eulerAngles);
        }
    }

    public void StopRotation()
    {
        var rotationValue = rotatingSprite.rotation.eulerAngles.z % 360;
        var normalizedTargetRotation = targetRotation % 360;

        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(rotationValue, normalizedTargetRotation));

        if (angleDifference <= threshold)
        {
            FinishMiniGame();
            rotatingSprite.rotation = Quaternion.Euler(0, 0, normalizedTargetRotation);
        }
    }

    public override void FinishMiniGame()
    {
        base.FinishMiniGame();
        isActive = false;
        Debug.Log("You made it!");
    }
}