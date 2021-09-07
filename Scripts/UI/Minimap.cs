using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[System.Serializable]
public class MinimapLegendEntry
{
	public string tagName;
	public Color color;
	public Vector2 size;
}
public class Minimap : MonoBehaviour
{
	public GameObject dot;
	public float updateRate;
	public List<MinimapLegendEntry> minimapLegend = new List<MinimapLegendEntry>();
	
	private RectTransform minimapBlock;
	private Dictionary<GameObject, GameObject> iconsToObjectsLink =
		new Dictionary<GameObject, GameObject>();

	void Awake()
    {
        minimapBlock = GetComponent<RectTransform>();
        InvokeRepeating(nameof(MoveIcons), updateRate, updateRate);
    }
    
    private Vector3 ConvertWorldToMap(GameObject convertingGameObject)
    {
	    Vector3 posObj = convertingGameObject.transform.position;
	    float width = - ConstantValues.MoveBorderLeft + ConstantValues.MoveBorderRight;
	    float height = ConstantValues.MoveBorderUp - ConstantValues.MoveBorderDown; 
	    Vector2 normalizedCoords = new Vector2 ((posObj.x - ConstantValues.MoveBorderLeft)/ width, (posObj.y - ConstantValues.MoveBorderDown)/ height);
	    var rect = minimapBlock.rect;
	    Vector3 ans = new Vector3(normalizedCoords.x * rect.width + rect.x,
		    normalizedCoords.y * rect.height + rect.y, 0);

	    return ans;
    }

    private void MoveIcons()
    {
	    List<GameObject> displayableObjects = new List<GameObject>();
	    foreach (MinimapLegendEntry curr in minimapLegend)
	    {
		    displayableObjects.AddRange(GameObject.FindGameObjectsWithTag(curr.tagName));
	    }
	    
	    List<GameObject> itemsToAdd = displayableObjects.FindAll(x => !iconsToObjectsLink.ContainsKey(x));
	    List<GameObject> itemsToDelete = new List<GameObject>();
	    foreach (KeyValuePair<GameObject, GameObject> curr in iconsToObjectsLink)
	    {
		    if (!displayableObjects.Contains(curr.Key))
				itemsToDelete.Add(curr.Key);
	    }

	    UpdateMap(itemsToAdd, itemsToDelete);
    }

    private void UpdateMap(List<GameObject> newItems, List<GameObject> outdatedItems)
    {
	    foreach (GameObject curr in outdatedItems)
	    {
		    Destroy(iconsToObjectsLink[curr]);
		    iconsToObjectsLink.Remove(curr);
	    }
	    
	    foreach (GameObject curr in newItems)
	    {
		    MinimapLegendEntry dotSettings = new MinimapLegendEntry();
		    foreach (MinimapLegendEntry x in minimapLegend)
		    {
			    if (curr.CompareTag(x.tagName))
				    dotSettings = x;
		    }
		    
		    
		    GameObject newDot = Instantiate(dot, transform);
		    newDot.name = curr.name;
		    newDot.GetComponent<RectTransform>().sizeDelta = dotSettings.size;
		    newDot.GetComponent<Image>().color = dotSettings.color;
		    iconsToObjectsLink.Add(curr, newDot);
	    }

	    foreach (KeyValuePair<GameObject, GameObject> curr in iconsToObjectsLink)
	    {
		    curr.Value.GetComponent<RectTransform>().localPosition = ConvertWorldToMap(curr.Key);
	    }
	    
    }
}
