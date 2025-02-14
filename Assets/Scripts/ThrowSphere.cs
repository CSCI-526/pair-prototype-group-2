using UnityEngine;

public class ThrowSphere : MonoBehaviour
{
    public GameObject spherePrefab; // 预制体（球体）
    public float throwForce = 10f; // 抛出力度
    public PlayerController playerController;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 检测鼠标左键点击
        {
            Throw();
        }
    }

    void Throw()
    {
        if (playerController == null)
        {
            Debug.LogError("PlayerController reference is missing!");
            return;
        }
        Debug.Log("create sphere..."); // 调试信息

        if (playerController.ballCount > 0)
        {
            Debug.Log("Creating sphere...");
            
            if (spherePrefab == null)
            {
                Debug.LogError("error：Sphere Prefab no value");
                return;
            }

            // 计算生成位置（摄像机前方 1.5 米）
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.5f;

            // 生成球体
            GameObject sphere = Instantiate(spherePrefab, spawnPosition, Quaternion.identity);

            Debug.Log("sphere created：" + sphere.name); // 确保实例化成功

            Rigidbody rb = sphere.GetComponent<Rigidbody>();
            if (rb == null)
            {
                Debug.LogError("error: sphere has no rigid body component！");
                return;
            }

            // 使用 Raycast 计算鼠标指向的方向
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 targetPoint;

            targetPoint = ray.GetPoint(50);
            // Debug.Log("hit point =  " + hit.point);
            // 计算抛出方向（目标点 - 球体当前位置）
           Vector3 throwDirection = (targetPoint - spawnPosition).normalized;

        // 施加一个向目标点的力
            rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

            Debug.Log("球体朝 " + targetPoint + " 抛出！");
            playerController.AddBallCount(-1);
        }
        else
        {
            Debug.Log("No more balls left!");
        }
    }
}
