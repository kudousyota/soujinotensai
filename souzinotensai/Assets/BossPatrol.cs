using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    public float reachDistance = 0.05f;

    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0) return;

        Vector2 targetPos = waypoints[currentWaypointIndex].position;
        Vector2 currentPos = rb.position;

        Vector2 direction = Vector2.zero;

        float deltaX = targetPos.x - currentPos.x;
        float deltaY = targetPos.y - currentPos.y;

        // X軸の移動が必要なら先にX方向に動く（Yは0）
        if (Mathf.Abs(deltaX) > reachDistance)
        {
            direction = new Vector2(Mathf.Sign(deltaX), 0);
        }
        // Xは合っていればY方向へ移動
        else if (Mathf.Abs(deltaY) > reachDistance)
        {
            direction = new Vector2(0, Mathf.Sign(deltaY));
        }
        else
        {
            // 到達したら次のポイントへ
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            return;
        }

        // 移動処理
        rb.MovePosition(currentPos + direction * speed * Time.fixedDeltaTime);
    }
}
