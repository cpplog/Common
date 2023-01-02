#pragma once
#include <vector>
#include <string>
#include <algorithm>

class CharyArray {
public:
	CharyArray(const std::vector<std::string>& source) :
		cstring_array(source.size())
	{
		std::transform(source.begin(), source.end(), cstring_array.begin(), [](const auto& elem_str) {
			char* buffer = new char[elem_str.size() + 1];
			std::copy(elem_str.begin(), elem_str.end(), buffer);
			buffer[elem_str.size()] = 0;
			return buffer;
			});
	}

	CharyArray(const CharyArray& other) = delete;
	CharyArray& operator=(const CharyArray& other) = delete;

	CharyArray(CharyArray&& other) = default;
	CharyArray& operator=(CharyArray&& other) = default;

	char** data() {
		return cstring_array.data();
	}

	size_t size() 
	{
		return cstring_array.size();
	}

	~CharyArray() {
		for (char* elem : cstring_array) {
			delete[] elem;
		}
	}

private:
	std::vector<char*> cstring_array;
};