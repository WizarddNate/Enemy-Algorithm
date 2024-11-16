using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GridArray : MonoBehaviour
{
    public float[,] gridCords = new float[2, 4] { { 1, 2, 3, 4 }, { 5, 6, 7, 8 } };

    public int num = 5;

    //Basic binary search functioin
    class GFG
    {

        // Returns index of x if it is present in arr[]
        static int binarySearch(float[] arr, int x)
        {
            int low = 0, high = arr.Length - 1;
            while (low <= high)
            {
                int mid = low + (high - low) / 2;

                // Check if x is present at mid
                if (arr[mid] == x)
                    return mid;

                // If x greater, ignore left half
                if (arr[mid] < x)
                    low = mid + 1;

                // If x is smaller, ignore right half
                else
                    high = mid - 1;
            }

            // If we reach here, then element was
            // not present
            return -1;
        }

        // Driver code
        /*public static void Main()
        {
  
            int result = binarySearch(gridCords, num);
            if (result == -1)
                Debug.Log("Element is not present in array");
            else
                Debug.Log("Element is present at " + "index " + result);
        } */
    }

}
