using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WorldObjectManager : MonoBehaviour {

    public Transform worldInsideTransform;
    public Transform worldOutsideTransform;

    public Transform changerTransform;
    public Transform playerTransform;

    [SerializeField]
    private TexturizerState _State;
    public TexturizerState State
    {
        get { return _State; }
        set { _State = value; UpdateShaderParamsAll(); }
    }

    [SerializeField]
    private string _ShaderName;
    public string ShaderName
    {
        get { return _ShaderName; }
        set { _ShaderName = value; UpdateShaderParamsAll(); }
    }

    [SerializeField]
    private Color _BorderColor;
    public Color BorderColor
    {
        get { return _BorderColor; }
        set { _BorderColor = value; UpdateShaderParamsAll(); }
    }

    [SerializeField]
    private float _BorderWidth;
    public float BorderWidth
    {
        get { return _BorderWidth; }
        set { _BorderWidth = value; UpdateShaderParamsAll(); }
    }

    [SerializeField]
    private Dimension _PrimaryColor; // True = blue, False = orange
    public Dimension PrimaryColor
    {
        get { return _PrimaryColor; }
        set { _PrimaryColor = value; UpdateShaderParamsAll(); }
    }

    [SerializeField]
    private float _LightRadius; // True = blue, False = orange
    public float LightRadius
    {
        get { return _LightRadius; }
        set { _LightRadius = value; UpdateShaderParamsAll(); }
    }

    private Dimension PlayerDimension;


	// Use this for initialization
	void Start () {
        UpdateShaderParamsAll();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateShaderParamsAll();
	}

    private void UpdateShaderParamsAll()
    {
        UpdateShaderParamsRecursive(worldInsideTransform, Dimension.BLUE);
        UpdateShaderParamsRecursive(worldOutsideTransform, Dimension.ORANGE);
    }

    private void UpdateShaderParamsRecursive(Transform t, Dimension dim)
    {
        foreach (Transform c in t)
        {
            UpdateShaderParams(c,dim);
            UpdateShaderParamsRecursive(c,dim);
        }
    }

    private void UpdateShaderParams(Transform obj, Dimension dim)
    {
        // Shader Code
#if UNITY_EDITOR
        Material mat = obj.GetComponent<Renderer>().sharedMaterial;
#else
        Material mat = obj.GetComponent<Renderer>().material;
#endif
        if (!mat.shader.name.Equals(ShaderName))
            return;

        mat.SetVector("_LightPos", changerTransform.position);
        mat.SetFloat("_TexturizerState", State == TexturizerState.RADIAL ? 1 : 0);
        bool Invert = dim != _PrimaryColor;
        mat.SetFloat("_Inverted", Invert ? 1 : 0);
        mat.SetFloat("_LightRad", _LightRadius);
        mat.SetColor("_BorderColor", _BorderColor);
        mat.SetFloat("_BorderWidth", _BorderWidth);

        // Collision Code
#if !UNITY_EDITOR
        BoxCollider box = obj.GetComponent<BoxCollider>();
        if (box != null)
            box.enabled = PlayerDimension == dim;
#endif
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(changerTransform.position,_LightRadius);
    }

    [System.Serializable]
    public enum TexturizerState
    {
        RADIAL, FLAT
    }

    [System.Serializable]
    public enum Dimension
    {
        BLUE, ORANGE
    }
}
