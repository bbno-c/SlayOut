using UnityEngine;

namespace Objects
{
	public class Bullet : MonoBehaviour
	{
		public SpriteRenderer SpriteRenderer;
		public Animator Animator;
		public float Speed;
		public float LifeTime;
		public float Damage;
		public Rigidbody2D Rigidbody;

		private float _timer;

		private void OnEnable()
		{
			SpriteRenderer.flipX = false;
			Animator.SetTrigger("Spawned");
			_timer = LifeTime;
			Rigidbody.velocity = transform.right * Speed;
		}

		private void Update()
		{
			_timer -= Time.deltaTime;
			if(_timer <= 0f)
            {
				OnBulletHit();
			}

			if (Animator.GetCurrentAnimatorStateInfo(0).IsName("BulletM16"))
				gameObject.SetActive(false);
		}
        private void OnBulletHit()
        {
			Animator.SetTrigger("Collide");
			SpriteRenderer.flipX = true;

		}

        private void OnCollisionEnter(Collision collision)
		{
			var health = collision.gameObject.GetComponentInParent<Health>();
			if(health != null) 
				health.Damage(Damage);

			gameObject.SetActive(false);
		}
	}
}
