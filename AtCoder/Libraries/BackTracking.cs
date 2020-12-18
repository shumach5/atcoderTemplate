using System.Collections.Generic;
using System.Linq;

namespace AtCoder.Libraries
{
    public class BackTracking
    {
		public IList<IList<int>> GetSubsets(int[] nums)
		{
			for (int i = 0; i <= nums.Length; i++)
			{
				SubSetsRecursive(nums, new List<int>(), 0, i);
			}

			return _output;
		}

		private void SubSetsRecursive(int[] nums, List<int> temp, int first, int num)
		{
			if (temp.Count == num)
			{
				_output.Add(temp.ToList());
				return;
			}

			for (int i = first; i < nums.Length; i++)
			{
				temp.Add(nums[i]);
				SubSetsRecursive(nums, temp, i + 1, num);
				temp.RemoveAt(temp.Count - 1);
			}
		}

		public IList<IList<int>> GetPermute(int[] nums)
		{
			PermuteRecursive(nums, 0);
			return _output;
		}

		void PermuteRecursive(int[] nums, int first)
		{
			if (first == nums.Length)
			{
				_output.Add(nums.ToList());
			}

			for (int i = first; i < nums.Length; i++)
			{
				(nums[first], nums[i]) = (nums[i], nums[first]);
				PermuteRecursive(nums, first + 1);
				(nums[first], nums[i]) = (nums[i], nums[first]);
			}
		}

		public IList<IList<int>> _output = new List<IList<int>>();
	}
}
