using UnityEngine;

public class ScriptBlock : MonoBehaviour {

    //config
    [SerializeField] private AudioClip breaksound;

    [SerializeField] private GameObject BlocksparkVFX;
    [SerializeField] private int points = 1;
    [SerializeField] private Sprite[] hitSprited;

    [SerializeField] private float move_min = -4;
    [SerializeField] private float move_max;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float timeoffset = 0f;

    //cache
    private Level level;

    private GameSession gamestate;

    //state
    private int timesHits; //TODO serlized for debug

    private void Start() {
        CountBreakAbleBlocks();
    }

    private void Update() {
        if (tag == "UnBreakable_m" || tag == "Breakable_m") {
            Vector3 pos1 = new Vector2(move_min, transform.position.y);
            Vector3 pos2 = new Vector2(move_max, transform.position.y);
            transform.position = Vector2.Lerp(pos1, pos2, (Mathf.Sin(speed * (Time.time+ timeoffset)) + 1.0f) / 2.0f);
        }
        
    }

    private void CountBreakAbleBlocks() {
        level = FindObjectOfType<Level>();
        gamestate = FindObjectOfType<GameSession>();
        if (tag == "Breakable" || tag == "Breakable_m") {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        AudioSource.PlayClipAtPoint(breaksound, Camera.main.transform.position);

        if (tag == "Breakable" || tag == "Breakable_m") {
            HandleHit();
        }
    }

    private void HandleHit() {
        timesHits++;
        int maxHits = hitSprited.Length + 1;
        if (timesHits >= maxHits) {
            DestroyBlock();
        } else {
            showNextHitSprite();
        }
    }

    private void showNextHitSprite() {
        int spriteIndex = timesHits - 1;
        if (hitSprited[spriteIndex] != null) {
            GetComponent<SpriteRenderer>().sprite = hitSprited[spriteIndex];
        } else {
            Debug.LogError("Block sprite is missing" + gameObject.name);
        }
    }

    private void DestroyBlock() {
        TriggerSparklesVXF();
        level.BlockBroken();
        gamestate.AddToScore(points);
        //AudioSource.PlayClipAtPoint(breaksound, new Vector3(transform.position.x, transform.position.y, Camera.main.transform.position.z));
        Destroy(gameObject);
    }

    private void TriggerSparklesVXF() {
        GameObject sparkles = Instantiate(BlocksparkVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}