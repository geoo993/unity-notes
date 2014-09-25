	private void clickPixel ()
	{
		if (!Input.GetMouseButtonDown (0))
			return;
		
		RaycastHit hit;
		if (!Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit))
			return;
		
		Renderer renderer = hit.collider.renderer;
		MeshCollider meshCollider = hit.collider as MeshCollider;
		if (renderer == null || renderer.sharedMaterial == null || renderer.sharedMaterial.mainTexture == null || meshCollider == null)
			return;
		
		Vector2 pixelUV = hit.textureCoord;
		int x = (int)(pixelUV.x * tex.width);
		int y = (int)(pixelUV.y * tex.height);
		Debug.Log (x + " " + y);
	}