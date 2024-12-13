//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Cinemachine;
//public class AimingVIewmanager : MonoBehaviour
//{
//    public Cinemachine.AxisState xAxis, yAxis;
//    [SerializeField] Transform MainCamera;
//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        xAxis.Update(Time.deltaTime);
//        yAxis.Update(Time.deltaTime);
//    }

//    private void LateUpdate()
//    {
//        MainCamera.localEulerAngles = new Vector3(yAxis.Value, MainCamera.localEulerAngles.y, MainCamera.localEulerAngles.z);
//        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
//    }
//}
