using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class End : GenericTask
{
	
	private new void Start()
	{
		ScoreManager.Instance.score = 0;
		SceneManager.LoadScene(0);
	}

	protected override void SpecificUpdate()
	{
	}

	protected override float CalculateTaskProgress()
	{
		return 0f;
	}

	protected override void OnKeyPressed()
	{
	}

	protected override void OnKeyUnpressed()
	{
	}
	
}
