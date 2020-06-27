using UnityEngine;

namespace Objects
{
	public class Bullet : MonoBehaviour
	{
		public float Speed = 5f;
		public float LifeTime = 5f;
		public float Damage = 10f;
		public Rigidbody Rigidbody;

		private float _timer;

		private void OnEnable()
		{
			_timer = LifeTime;
			Rigidbody.velocity = transform.TransformDirection(Vector3.forward * Speed);
		}

		private void Update()
		{
			_timer -= Time.deltaTime;
			if(_timer <= 0f) 
				Destroy(gameObject);
		}

		private void OnCollisionEnter(Collision collision)
		{
			var health = collision.gameObject.GetComponentInParent<Health>();
			if(health != null) 
				health.Damage(Damage);

			Destroy(gameObject);
		}
	}
}
