using UnityEngine;

namespace DNSCoreMechanics.CursorManager
{
    public class CursorManager : MonoBehaviour
    {
        [Header("Cursor Customize Settings")]
        [SerializeField] Texture2D cursorTexture;
        [SerializeField] Vector2 cursorHotspot;

        [Header("Trail to Mouse Settings")]
        [SerializeField] Vector2 distanceBetweenCursor;
        [SerializeField] GameObject[] Points;
        [SerializeField] GameObject pointPrefab;
        [SerializeField] int numberOfPoints;


        void Start()
        {
            cursorHotspot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
            Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
        }

        //TODO: Finalize this implementation
        public void CreateTrailPoints()
        {
            Points = new GameObject[numberOfPoints];
            for (int i=0; i < numberOfPoints; i++)
            {
                Points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
                Points[i].transform.parent = this.gameObject.transform;
            }
        }

        public void LookAtMousePos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            distanceBetweenCursor = new Vector2(mousePos.x - transform.position.x, mousePos.y -transform.position.y);
            transform.up = distanceBetweenCursor;
        }
    }
}

