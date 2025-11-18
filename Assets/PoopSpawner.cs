using UnityEngine;

public class PoopSpawner : MonoBehaviour
{
    public GameObject[] poopPrefabs; // Lista de prefabs de Poop
    public float spawnInterval = 10f; // Intervalo de spawn en segundos
    public AudioClip poopSound;
    private AudioSource audioSource;
    private Animator animator;
    private float timer;
    private float poopyResetTimer = 0f;
    public float poopyAnimDuration = 2f; // Duración de la animación Poopy (ajusta según tu animación)

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator != null)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("BreathingIdle"))
            {
                // Si está en Poopy, espera a que termine para resetear el bool
                if (stateInfo.IsName("Poopy"))
                {
                    poopyResetTimer += Time.deltaTime;
                    if (poopyResetTimer >= poopyAnimDuration)
                    {
                        animator.SetBool("POOPY", false);
                        poopyResetTimer = 0f;
                    }
                }
                return; // Detiene el contador si no está en BreathingIdle
            }
        }
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnPoop();
        }
    }
private void SpawnPoop()
{
    if (poopPrefabs != null && poopPrefabs.Length > 0)
    {
        for (int i = 0; i < 5; i++)
        {
            int randomIndex = Random.Range(0, poopPrefabs.Length);
            GameObject poop = Instantiate(poopPrefabs[randomIndex], transform.position, Quaternion.identity, transform);
        }
    }
    if (audioSource != null && poopSound != null)
    {
        audioSource.PlayOneShot(poopSound);
    }
    if (animator != null)
    {
        animator.SetBool("POOPY", true);
    }
}
}