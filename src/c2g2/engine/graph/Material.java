package c2g2.engine.graph;

import org.joml.Vector3f;

import c2g2.engine.graph.Texture;

public class Material {

	private static final Vector3f DEFAULT_COLOUR = new Vector3f(1.0f, 1.0f, 1.0f);

	private Vector3f colour;

	private float reflectance;

	// RGB texture mapping
	private Texture texture;

	// Normal texture mapping
	private Texture normTexture;

	public Material() {
		colour = DEFAULT_COLOUR;
		reflectance = 0;
	}

	public Material(Vector3f colour, float reflectance) {
		this();
		this.colour = colour;
		this.reflectance = reflectance;
	}

	public void setTexture(Texture texture) {
		this.texture = texture;
	}

	public Texture getTexture() {
		return texture;
	}

	public void setNormTexture(Texture texture) {
		this.normTexture = texture;
	}

	public Texture getNormTexture() {
		return normTexture;
	}

	public Vector3f getColour() {
		return colour;
	}

	public void setColour(Vector3f colour) {
		this.colour = colour;
	}

	public float getReflectance() {
		return reflectance;
	}

	public void setReflectance(float reflectance) {
		this.reflectance = reflectance;
	}

	public boolean isTextured() {
		return texture != null;
	}

	public boolean hasNormTexture() {
		return normTexture != null;
	}
}
