using UnityEngine;

public class TimeboosArea : MonoBehaviour
{
    public float time_to_add = 10.0f;
    public float roation_speed = 250.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    /* void FixedUpdate()
    {
        transform.Rotate(Vector3.up*roation_speed * Time.deltaTime, Space.Self);
    }
    */

    private void OnTriggerEnter(Collider other){
        PlayerBehaviour player = other.GetComponentInParent<PlayerBehaviour>();
        if (player != null){
            //On appelle la fonction gamemode
            GameMode.add_time(time_to_add);
            this.gameObject.SetActive(false);
        }
    }
}
