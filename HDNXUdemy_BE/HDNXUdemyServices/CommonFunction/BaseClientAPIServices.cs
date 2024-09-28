using HDNXUdemyModel.Exception;
using HDNXUdemyModel.Exceptions;
using HDNXUdemyModel.SystemExceptions;
using Microsoft.Rest;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HDNXUdemyServices.CommonFunction
{
    public class BaseClientAPIServices : ServiceClient<BaseClientAPIServices>
    {
        private Dictionary<string, List<string>>? _headers;
        private AuthenticationHeaderValue? _authenticationHeader;

        /// <summary>
        /// ClientAPIServices
        /// </summary>
        /// <param name="header"></param>
        /// <param name="authenticationHeader"></param>
        /// <exception cref="ProjectException"></exception>
        public BaseClientAPIServices(Dictionary<string, List<string>> header, AuthenticationHeaderValue authenticationHeader)
        {
            _headers = header;
            _authenticationHeader = authenticationHeader;
            if (_authenticationHeader != null)
            {
                this.HttpClient.DefaultRequestHeaders.Authorization = _authenticationHeader;
            }
        }

        private async Task<HttpResponseMessage> Get(string url, CancellationToken cancellationToken = default)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
            cancellationToken.ThrowIfCancellationRequested();
            var response = await HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            return response;
        }

        private async Task<HttpResponseMessage> Delete(string url, CancellationToken cancellationToken = default)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, new Uri(url));
            cancellationToken.ThrowIfCancellationRequested();
            var response = await HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
            return response;
        }

        private async Task<HttpResponseMessage> Post<T>(string url, T data, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await HttpClient.PostAsJsonAsync(url, data, cancellationToken).ConfigureAwait(false);
            return response;
        }

        private async Task<HttpResponseMessage> Put<T>(string url, T data, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var response = await HttpClient.PutAsJsonAsync(url, data, cancellationToken).ConfigureAwait(false);
            return response;
        }

        public async Task<T?> Get<T>(string url, CancellationToken cancellationToken = default)
        {
            var returnData = default(T);
            try
            {
                using (HttpResponseMessage response = await Get(url, cancellationToken))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new AuthenticationException(System.Net.HttpStatusCode.Unauthorized.GetEnumDescription());
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new ProjectNotFoundException(System.Net.HttpStatusCode.NoContent.GetEnumDescription());
                    }
                    var returnDataCallAPI = await response.Content.ReadAsStringAsync();
                    returnData = JsonConvert.DeserializeObject<T>(returnDataCallAPI);
                    return returnData;
                }
            }
            catch (Exception ex)
            {
                throw new ProjectException(ex.Message, ex);
            }
        }

        public async Task<V?> Post<T, V>(string url, T data, bool isThrowException = false, CancellationToken cancellationToken = default)
        {
            var result = default(V);
            try
            {
                using (HttpResponseMessage response = await Post(url, data, cancellationToken))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (isThrowException && response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        var messenger = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ProjectBadRequestException(messenger);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new AuthenticationException(System.Net.HttpStatusCode.Unauthorized.GetEnumDescription());
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new ProjectNotFoundException(System.Net.HttpStatusCode.NoContent.GetEnumDescription());
                    }

                    var resultData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<V>(resultData);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ProjectException(ex.Message, ex);
            }
        }

        public async Task<V?> Put<T, V>(string url, T data, bool isThrowException = false, CancellationToken cancellationToken = default)
        {
            var result = default(V);
            try
            {
                using (HttpResponseMessage response = await Put(url, data, cancellationToken))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (isThrowException && response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        var messenger = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        throw new ProjectBadRequestException(messenger);
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new AuthenticationException(System.Net.HttpStatusCode.Unauthorized.GetEnumDescription());
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new ProjectNotFoundException(System.Net.HttpStatusCode.NoContent.GetEnumDescription());
                    }

                    var resultData = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<V>(resultData);
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ProjectException(ex.Message, ex);
            }
        }

        public async Task<T?> Delete<T>(string url, CancellationToken cancellationToken = default)
        {
            var returnData = default(T);
            try
            {
                using (HttpResponseMessage response = await Delete(url, cancellationToken))
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        throw new AuthenticationException(System.Net.HttpStatusCode.Unauthorized.GetEnumDescription());
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        throw new ProjectNotFoundException(System.Net.HttpStatusCode.NoContent.GetEnumDescription());
                    }
                    var returnDataCallAPI = await response.Content.ReadAsStringAsync();
                    returnData = JsonConvert.DeserializeObject<T>(returnDataCallAPI);
                    return returnData;
                }
            }
            catch (Exception ex)
            {
                throw new ProjectException(ex.Message, ex);
            }
        }
    }
}