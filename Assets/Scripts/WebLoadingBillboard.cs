using System.Collections;
using UnityEngine;

public class WebLoadingBillboard : MonoBehaviour {

    public string weburl;

	public void Start()
    {
        Managers.Images.GetWebImage(OnWebImage, weburl);
    }

    private void OnWebImage(Texture2D image)
    {
        float imageheight = (float) (image.height * .001);
        float imagewidth = (float) (image.width * .001);
        //Debug.Log(imagewidth.ToString() + ", " + imageheight.ToString());
        GetComponent<Renderer>().material.mainTexture = image;
        //transform.localScale = new Vector3(.2f, , 1);
        float existingdepth = transform.parent.localScale.x;
        float existingwidth = transform.parent.localScale.z;
        float existingheight = transform.parent.localScale.y;
        //Debug.Log("Existing width: " + existingwidth);
        transform.parent.transform.localScale = new Vector3(existingdepth, imageheight / imagewidth * existingheight, existingwidth);
        //Debug.Log(name);
    }
}
