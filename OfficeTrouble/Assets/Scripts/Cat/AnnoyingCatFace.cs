using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnnoyingCatFace : MonoBehaviour
{

	[SerializeField] private Sprite catFaceImage;
	[SerializeField] private Sprite otterFaceImage;

	private Animator _animator;
	
	private const float InitialDelaySeconds = 60f;
	private const float DelayBetweenSpawnSeconds = 30f;
	private const float OtterProbability = 0.2f;
	private const float SpawnProbability = 0.005f;
	
	private float _secondsSinceStart;
	private bool _cative;
	private bool _catCurrentlyPresent;

	private void Start()
	{
		_animator = GetComponent<Animator>();
	}

	private void FixedUpdate()
	{
		
		if (_cative && !_catCurrentlyPresent)
		{
			float spawnValue = Random.Range(0f, 1f);
			Debug.Log(spawnValue);
			if (spawnValue <= SpawnProbability)
			{
				float otterValue = Random.Range(0f, 1f);
				if (otterValue < OtterProbability)
				{
					GetComponent<Image>().sprite = otterFaceImage;
				}
				else
				{
					GetComponent<Image>().sprite = catFaceImage;
				}

				_catCurrentlyPresent = true;
				_animator.SetBool(Animator.StringToHash("active"), true);
				
				StartCoroutine(AnimationDelay());
			}
		}

	}

	private void Update()
	{

		_secondsSinceStart += Time.deltaTime;
		if (_secondsSinceStart >= InitialDelaySeconds)
		{
			_cative = true;
		}

	}

	private IEnumerator AnimationDelay()
	{
		yield return new WaitForSeconds(9f);
		
		_catCurrentlyPresent = false;
		_animator.SetBool(Animator.StringToHash("active"), false);
		_cative = false;
		_secondsSinceStart = InitialDelaySeconds - DelayBetweenSpawnSeconds;
	}

}
