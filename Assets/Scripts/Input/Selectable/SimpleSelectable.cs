﻿using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shows outline when selected
/// </summary>
public abstract class SimpleSelectable : MonoBehaviour, ISelectable
{
    public class Outline
    {
        public Renderer renderer;
        public Material prevMaterial;

        public Outline(Renderer renderer, Material prevMaterial)
        {
            this.renderer = renderer;
            this.prevMaterial = prevMaterial;
        }

        public void Select() => renderer.material = MaterialDict.Instance.HighlightMaterial;

        public void DeSelect() => renderer.material = prevMaterial;
    }

    protected Outline[] outlines;

    protected virtual bool DisplayHighlight { get; } = true;

    private void Awake()
    {
        ScanForRenders();
        InnerAwake();
    }

    /// <summary>
    /// Scans for Renderes and places outlines on them
    /// </summary>
    protected virtual void ScanForRenders()
    {
        List<Renderer> renderers = new List<Renderer>();

        transform.GetComponentsInChildren(false, renderers);
        outlines = new Outline[renderers.Count];

        for (int i = 0; i < renderers.Count; i++)
        {
           outlines[i] = new Outline(renderers[i], renderers[i].material);
        }
    }

    protected virtual void InnerAwake() { }

    public virtual void Select()
    {
        if (DisplayHighlight == true)
            for (int i = 0; i < outlines.Length; i++)
                outlines[i].Select();
        InnerSelect();
    }

    protected virtual void InnerSelect() { }

    public virtual void DeSelect()
    {
        if (DisplayHighlight == true)
            for (int i = 0; i < outlines.Length; i++)
                outlines[i].DeSelect();
        InnerDeSelect();
    }

    protected virtual void InnerDeSelect() { }

}
